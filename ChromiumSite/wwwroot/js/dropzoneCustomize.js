function customize(path) {
    Dropzone.options.myDropzone = {
        url: path,
        autoProcessQueue: false,
        uploadMultiple: true,
        parallelUploads: 100,
        maxFiles: 100,
        acceptedFiles: "image/*",

        init: function () {

            var submitButton = document.querySelector("#submit-all");
            var wrapperThis = this;

            submitButton.addEventListener("click", function () {
                wrapperThis.processQueue();
            });

            this.on("addedfile", function (file) {

                // Create the remove button
                var removeButton = Dropzone.createElement("<button class='btn btn-danger btn-lg'><i class='glyphicon glyphicon-trash'></i></button>");
                // Listen to the click event
                removeButton.addEventListener("click", function (e) {
                    // Make sure the button click doesn't submit the form:
                    e.preventDefault();
                    e.stopPropagation();

                    // Remove the file preview.
                    wrapperThis.removeFile(file);
                    // If you want to the delete the file on the server as well,
                    // you can do the AJAX request here.
                });

                // Add the button to the file preview element.
                file.previewElement.appendChild(removeButton);
            });
        }
    };
}