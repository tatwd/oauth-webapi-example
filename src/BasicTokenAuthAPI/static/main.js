
// show alert container
function showAlert(el, status, msg) {
    if (status === 'success')
        el.addClass('show alert-success').prepend(msg)
    else if (status === 'error')
        el.addClass('show alert-danger').prepend(msg)
    else if (status === 'warn')
        el.addClass('show alert-warning').prepend(msg)
}

// save token in localStorage
function saveToken(key, token) {
    const data = {
        login_at: new Date().getTime(),
        is_loggon: true,
        auth_token: token,
    };
    localStorage.setItem(key, JSON.stringify(data));
}

// get token
function getToken(key) {
    const tokenStr = localStorage.getItem(key)

    if (!tokenStr)
        return null;

    let token = JSON.parse(tokenStr);
    let nowTime = new Date().getTime();
    let spanTime = nowTime - token.login_at;

    if (spanTime <= token.auth_token.expires_in)
        return token;
    else {
        token.is_loggon = false;
        localStorage.setItem(key, token); // update token state
        return token;
    }

}

function isLogged(key) {
    let token = getToken(key);

    return token.is_loggon;
}

$(function () {

    const REGISTER_URL = 'http://localhost:55194/api/account/register';
    const LOGIN_URL = 'http://localhost:55194/token';

    // register
    const register = function () {
        const btnRegister = $('#btn-register');
        const txtUsername = $('#username');
        const txtPassword = $('#password');
        const alertPlayground = $('#alert-playground > .alert');

        btnRegister.click((e) => {
            // get user info
            const user = {
                username: txtUsername.val(),
                password: txtPassword.val()
            };

            // post data to server by ajax
            $.post(REGISTER_URL, user, (data, status, xhr) => {
                console.log(data)

                // add alert info
                showAlert(alertPlayground, status, data);
            });

            e.preventDefault();
        });


    };

    register();


    // login 
    const login = function () {
        const btnLogin = $('#btn-login');
        const txtUsername = $('#username');
        const txtPassword = $('#password');
        const alertPlayground = $('#alert-playground > .alert');

        btnLogin.click((e) => {

            e.preventDefault();

            const user = {
                username: txtUsername.val(),
                password: txtPassword.val(),
                grant_type: 'password'
            }

            let data = user;
            
            let token = getToken(user.username);
            if (token) {
                if (token.is_loggon) {
                    showAlert(alertPlayground, 'warn', 'You have been logined!');
                    return;
                }

                data = {
                    grant_type: 'refresh_token',
                    refresh_token: token.auth_token.refresh_token
                };
            }

            $.ajax({
                type: 'POST',
                url: LOGIN_URL,
                data: data,
                success: (data, status, xhr) => {
                    console.log(data);

                    saveToken(user.username, data);

                    showAlert(alertPlayground, status, 'Login successfully!');
                },
                error: (xhr, status, error) => {
                    showAlert(alertPlayground, status, 'Login failed!');
                }
            });

        });
    };

    login();

    //let token = getToken("root");
    //let isLoggon = isLogged("root");
    //console.log(token, isLoggon);
});