
function deleteCheckFirst(deleteElements) {
    deleteElements.forEach((e) => {
        e.addEventListener("click", function (event) {
            event.preventDefault();
            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = this.href;


                }
            });
        });
    });
}




if (location.pathname.includes("/Order/ShowOrder")) {
    let element = document.querySelectorAll(".action-btn");
    if (element)
        deleteCheckFirst(element);
}
else if (location.pathname.includes("/Insert") || location.pathname.includes("/GetAll")) {
    let element = document.querySelectorAll(".text .delete");
    if (element)
        deleteCheckFirst(element);
}


