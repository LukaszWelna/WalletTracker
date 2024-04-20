/**
 * 
 * Replace dots with commas in the input fields
 * 
 */
$("#Amount").on("input", function () {
    let value = $(this).val();
    $(this).val(value.replace(/\./g, ','));
});
