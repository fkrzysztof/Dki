$("body").on("click", ".removeButton", function (event) {
    event.preventDefault();
    let toHide = $(this).closest(".hide-show-item");
    let send = $(this).data("id");
    $.post("/Offer/DeleteJS", { id: send }, function (data) {
        if (data == true) {
            toHide.fadeOut();
        }
        else {
            alert("Wystąpił problem..");
        }
    });
});