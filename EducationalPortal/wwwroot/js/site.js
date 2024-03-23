// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const bsButton = new bootstrap.Button('#myButton')
document.getElementById('confirmPassword').addEventListener('input', function () {
        var password = document.getElementById('Password').value;
        var confirmPassword = this.value;
        var errorSpan = document.getElementById('confirmPasswordError');

        if (password !== confirmPassword) {
            errorSpan.innerText = 'Passwords do not match.';
        } else {
            errorSpan.innerText = '';
        }
    });