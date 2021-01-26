// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("TransactionTypeVM").addEventListener("change",
    function (obj) {
        var input = document.getElementById("DestAccount");
        input.disabled = !(this.value == "3");
    }, false);