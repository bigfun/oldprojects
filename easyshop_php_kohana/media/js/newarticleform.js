
$().ready(function() {
	// validate the comment form when it is submitted
	// validate signup form on keyup and submit
	$("#newArticleForm").validate({
		rules: {
			title: "required",
            body: "required"
		},
		messages: {

			title: {
				required: "Podaj tytuł artykułu"
			},
			body: {
                required: "Podaj treść artykułu"
            }
		}

	});
});
