
$().ready(function() {
	// validate the comment form when it is submitted
	// validate signup form on keyup and submit
	$("#loginForm").validate({
		rules: {
			password: "required",

            mail: {
                required: true,
                email: true
            }
		},
		messages: {

			password: {
				required: "Podaj hasło do konta"
			},
			mail: {
                email: "Podaj poprawny adres email",
                required: "Podaj adres email"
            }
		}

	});
});
