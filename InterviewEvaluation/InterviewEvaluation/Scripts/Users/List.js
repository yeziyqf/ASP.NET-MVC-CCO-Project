// Add JavaScript evaluation section changes here.
// Solution:

function getSearch() {
    console.log($("#query").val())
    console.log(typeof ($("#query").val()))
    $.ajax({
        type: "POST",
        url: "./Search",
        data: { query: $("#query").val() },
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });
}

function OnSuccess(response) {
    // Situation for no matching results.
    if (response.length == 0) {
        document.getElementById('UserList').options.length = 0
        $("#SearchResultID").empty("");
        $("#SearchResultEmail").empty("");
        alert("No matching results.");
    }
        // Valid results handling.
    else {
        //alert("Success!");
        console.log(response);
        console.log('response Text: ' + response[0].Text);
        console.log('response Value: ' + response[0].Value);

        // Dropdown List
        // Delete old results
        document.getElementById('UserList').options.length = 0

        var select = $("#UserList")

        $.each(response, function (i) {
            var option = document.createElement('option');
            option.text = response[i].Text;
            option.value = response[i].Value;
            select.append(option);
        })

        // Search Result Table
        var SearchResultID = $("#SearchResultID")
        var ResultEmail = $("#SearchResultEmail")

        // Delete the orginal search results.
        $("#SearchResultID").empty("");
        $("#SearchResultEmail").empty("");

        // Update each column separately.
        $.each(response, function (i) {
            SearchResultID.append('<tr><td>' + response[i].Value + '</td></tr>');
            ResultEmail.append('<tr><td>' + response[i].Text + '</td></tr>');
        })
    }
}
// End solution.
// End changes.