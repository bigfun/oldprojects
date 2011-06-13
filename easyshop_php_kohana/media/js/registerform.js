
$().ready(function() {
	// validate the comment form when it is submitted
	// validate signup form on keyup and submit
	$("#registerForm").validate({
		rules: {
			name: "required",
			surname: "required",
			password: {
				required: true,
				minlength: 6
			},
            mail: {
                required: true,
                email: true
            },
			confirm_password: {
				required: true,
				minlength: 5,
				equalTo: "#password"
			},
            street: "required",
            city: "required",
            postcode: {
                required: true,
                minlength: 6,
                maxlength: 6
            },
            phone: "required"
		},
		messages: {

			name: "Podaj swoje imię",
			surname: "Podaj swoje nazwisko",
            phone: "Podaj swój telefon",
            street: "Podaj swój adres",
            city: "Podaj miasto zamieszkania",
            postcode: {
                required: "Podaj swój kod pocztowy",
                minlength: "Kod musi mieć 5 cyfr",
                maxlength: "Kod musi mieć 5 cyfr"
            },
			password: {
				required: "Podaj hasło do konta",
				minlength: "Hasło musi mieć min. 6 znaków"
			},
			confirm_password: {
				required: "Podaj hasło ponownie",
				minlength: "Hasło musi mieć min. 6 znaków",
				equalTo: "Podaj takie samo hasło"
			},
			mail: {
                email: "Podaj poprawny adres email",
                required: "Podaj adres email"
            }
		}

	});

	// check if confirm password is still valid after password changed
	$("#password").blur(function() {
		$("#confirm_password").valid();
	});

    $("#postcode").mask("99-999");
});
