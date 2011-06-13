
$().ready(function() {
	// validate the comment form when it is submitted
	// validate signup form on keyup and submit
	$("#newProductForm").validate({
		rules: {
			title: "required",
            description: "required",
            price: {
                required: true,
                numberDE: true

            }
		},
		messages: {

			title: {
				required: "Podaj tytuł artykułu"
			},
			description: {
                required: "Podaj opis produktu"
            },
            price: {
                required: "Podaj cenę produktu",
                numberDE: "cena musi być liczbą dziesiętną po przecinku"
            }
		}
	});


    				// Dialog
				$('#add_category_dialog').dialog({
					autoOpen: false,
					width: 600,
					buttons: {
						"Ok": function() {
                            $(this).dialog("close");
                            $("#categoryloader").text("Dodawanie...");
							$.get("addcategory/"+$("#categorytitle").val(),null,function(answer) {
                                handleAnswer(answer);
                            }
                        );
                        $(this).dialog("close");
						},
						"Cancel": function() {
							$(this).dialog("close");
						}
					},
                    modal: true
				});

				// Dialog Link
				$('#add_category_link').click(function(){
					$('#add_category_dialog').dialog('open');
					return false;
				});

				//hover states on the static widgets
				$('#add_category_link').hover(
					function() { $(this).addClass('ui-state-hover'); },
					function() { $(this).removeClass('ui-state-hover'); }
				);
});

function handleAnswer(answer)
{
     if(answer == "" || $("status",answer).text() == "1")
         {

         $("#categoryloader").text("Dodawanie nieudane.");
         $("#categoryloader").addClass("error");
         return;
         }
     $("#myselect").removeOption(/./);
     $("category",answer).each(function(id) {
       message = $("category",answer).get(id);
       $("#category").addOption($("id",message).text(),$("name",message).text());
     });
     $("#category").selectOptions($("#category option:last").val());
     
     $("#categoryloader").text("Dodano.");
     $("#categoryloader").addClass("success");
}
