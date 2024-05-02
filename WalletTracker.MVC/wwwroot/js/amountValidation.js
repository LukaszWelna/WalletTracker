// Add jQuery Validation plugin method for a amount

// Valid amount must contain max 2 digits after decimal point
// Valid amount must be greater than 0
// Limit must be greater or equal 0
// Valid amount must be lower than 100000000

$.validator.addMethod("decimalPlaces", function (value, element) {
    return (! /[0-9]*\,[0-9]{3,}/.test(value));
}, "Amount must contain max 2 digits after decimal point.");

$.validator.addMethod("amountMin", function (value, element) {
    var normalizedValue = parseFloat(value.replace(',', '.'));
    return (normalizedValue > 0);
}, "This field is required.");

$.validator.addMethod("limitMin", function (value, element) {
    var normalizedValue = parseFloat(value.replace(',', '.'));
    return (normalizedValue >= 0);
}, "Limit must be greater or equal to 0.");

$.validator.addMethod("amountMax", function (value, element) {
    var normalizedValue = parseFloat(value.replace(',', '.'));
    return (normalizedValue < 100000000);
}, "Please enter a value lower than 100000000.");