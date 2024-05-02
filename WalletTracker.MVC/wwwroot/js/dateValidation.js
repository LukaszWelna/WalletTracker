// Add jQuery Validation plugin method for a date
 
// Date must be valid (leap years etc.)
// Date must be equal or earlier than current date
// End Date must be equal or grater than start date

$.validator.addMethod("dateTime", function (value, element) {
    return (!isNaN(Date.parse(value)));
}, "Must be a valid date.");

$.validator.addMethod("dateNotGreaterThanToday", function (value, element) {
    var selectedDate = new Date(value);
    selectedDate.setHours(0, 0, 0, 0);
    var currentDate = new Date(new Date().toDateString());

    return (selectedDate <= currentDate);

}, "Date must be equal or earlier than current date.");

$.validator.addMethod("endDateGreaterThanStartDate", function (value, element) {
    var endDate = new Date(value);
    endDate.setHours(0, 0, 0, 0);
    var startDate = new Date($("#StartDate").val());
    startDate.setHours(0, 0, 0, 0);

    return (startDate <= endDate);

}, "End date must be equal or greater than start date.");