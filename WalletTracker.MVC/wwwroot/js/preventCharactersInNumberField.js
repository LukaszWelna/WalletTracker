$(function () {
    $("#Amount").on("keydown", function (event) {
        const invalidCharacters = ["+", "-", "E", "e"];

        if (invalidCharacters.includes(event.key)) {
            event.preventDefault();
        }
    })
});