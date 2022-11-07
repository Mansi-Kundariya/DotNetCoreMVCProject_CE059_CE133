// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const type = document.querySelectorAll(".activity__button");
const activity = document.querySelectorAll(".form__activity");

type.forEach(button => {

    button.addEventListener("click", e => {
        activity[0].value = button.textContent;
    })

})

if (activity[0] != null) {

    switch (activity[0].value) {
        case "Attraction":
            document.querySelector(".activity__button--Attraction").setAttribute("style", "text-decoration: underline")
            break;
        case "Event":
            document.querySelector(".activity__button--Event").setAttribute("style", "text-decoration: underline")
            break;
        case "Restaurant":
            document.querySelector(".activity__button--Restaurant").setAttribute("style", "text-decoration: underline")
            break;
    }
}