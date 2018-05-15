$(function () {

    const KEY = "USER_KEY";

    // login btn click event
    // use token +
    $('#login-btn').click(function (e) {
        const username_password = $('#username').val() + ':' + $('#password').val();
        const bscode = btoa(username_password);

        $.ajax({
            url: '/api/account/login',
            headers: {
                "Authorization": "Basic " + bscode
            },
            success: function (data) {
                console.log(data)
                localStorage.setItem(KEY, bscode)
            }
        });

        e.preventDefault();
    });


    // get products
    $('#fetch-products').click(function () {
        $.ajax({
            url: '/api/products',
            headers: {
                "Authorization": "Basic " + localStorage.getItem(KEY)
            },
            success: function (data) {
                console.log(data)

                let html = '';
                data.forEach(product => {
                    html += `
            <li class="list-group-item">
            #${product.Id} 
            **${product.Title}**
            <span class="badge badge-info align-text-top">￥${product.Price}</span>
            </li>
            `
                })

                $('#products').html(html);
            }
        });
    })
});
