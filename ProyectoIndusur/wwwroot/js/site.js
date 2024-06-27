// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    axios.get('/Home/GetData')
        .then(function (response) {
            console.log("entre a la funcion");
            document.getElementById('registros').innerText = response.data.Message;
        })
        .catch(function (error) {
            console.error('There was an error!', error);
        });
});
