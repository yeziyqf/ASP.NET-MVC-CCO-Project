// Add JavaScript evaluation section changes here.



// End changes.

// Solution:
function SearchUsers(query) {
    $.ajax({
        url: "/Users/Search",
        data: { query: $("#query").val() },
        success: function (result) {
            var select = $("#UserList")

            $.each(result, function (i) {
                var option = document.createElement('option');
                option.text = result[i].Text;
                option.value = result[i].Value;
                select.append(option);
            })
        }
    });
}
// End solution.