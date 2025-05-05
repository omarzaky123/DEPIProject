const dropdownlist = document.getElementById("ProductID");
const branchIdElement = document.getElementById("branchid");

dropdownlist.addEventListener("change", function () {
    const productId = this.value;
    const changingDropDownList = $('#VartionID');
    changingDropDownList.empty();
    changingDropDownList.append($('<option>', { value: '0', text: '-- Select Vartion --' }));

    $.ajax({
        url: "/Vartion/GetByProductId",
        data: { "productId": productId },
        success: function (results) {
            console.log(results); // Log the entire response to check structure

            // Check if results is an array
            if (Array.isArray(results)) {
                $.each(results, function (i, result) {
                    changingDropDownList.append(
                        $('<option>', {
                            value: result.id,
                            text: result.name
                        })
                    );
                });
            }
            // If results is an object (single item)
            else if (results && results.id) {
                changingDropDownList.append(
                    $('<option>', {
                        value: results.id,
                        text: results.name
                    })
                );
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    });
});