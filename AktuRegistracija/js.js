function show_if_selected() {
    selected_value = document.getElementById("Select1").value;
    if (selected_value === "Tirdzniecības punkts") {
        document.getElementById("tirdzniecibas_vieta_lauks").style.display = "block";
    }
    else {
        document.getElementById("tirdzniecibas_vieta_lauks").style.display = "none";
    }
}

function show_modal() {
    var modal = document.getElementById('myModal');
    modal.style.display = "block";
    window.onclick = function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }
}

function hide_modal() {
    var modal = document.getElementById('myModal');
    modal.style.display = "none";
}

function validate_IBAN_number()
{
    var IBAN_number = document.getElementById("bank_number").value;
    PageMethods.ValidateBankAccount(IBAN_number, onSucces, onError);
    function onSucces(result)
    {
        if (result === true) {
            document.getElementById("bank_number").style.backgroundColor = "rgba(0, 255, 0, 0.2)";
            console.log("Viss oks!");
            send_values_to_db();
        }
        else
        {
            swal("Bankas konta lauks nav aizpildīts, vai arī ievadītie dati nav pareizi", "", "error");
            document.getElementById("bank_number").classList.add("not-filled-border"); 
            document.getElementById("bank_number").focus();
            console.log("Nav derīgs bankas konts");

        }
    }
    function onError(result)
    {
        swal("Something went wrong!", "Try again later!", "error");
        console.log("Errors IBAN validate");
    }
}

function nosutam_datus()
{
    hide_modal();
    var email = document.getElementById("kontakti_epasts").value;
    PageMethods.IsEmail(email, onSuc, onErr);
    function onSuc(result)
    {   
        if (result === true) {
            console.log("Epasts ir derīgs");
            document.getElementById("kontakti_epasts").classList.remove("not-filled-border"); 
            validate_IBAN_number();
        }
        else
        {
            swal("Epasta adrese ievadīta neprecīzi!", "", "error");
            document.getElementById("kontakti_epasts").classList.add("not-filled-border");
            document.getElementById("kontakti_epasts").focus();
            console.log("Epasta adrese ievadīta neprecīzi!");
        }

    }
    function onErr(result)
    {
        swal("Something went wrong!", "Try again later!", "error");
        console.log("Errors Email Checks");
    }

}

function get_all_fields() {
    var currentDate = new Date()
    var day = currentDate.getDate()
    var month = currentDate.getMonth() + 1
    var year = currentDate.getFullYear()
    var dict = {};
    dict[0] = document.getElementById("pasakuma_nosaukums").value;
    dict[1] = document.getElementById("pasakuma_datums").value;
    dict[2] = document.getElementById("vards_uzvards").value;
    dict[3] = document.getElementById("personas_kods").value;
    dict[4] = document.getElementById("kontakti_epasts").value;
    dict[5] = document.getElementById("kontakti_tel").value;
    dict[6] = document.getElementById("bank_number").value;
    dict[7] = document.getElementById("swift_code").value;
    dict[8] = document.getElementById("bank_name").value;
    dict[9] = document.getElementById("ticket_price").value;
    dict[10] = document.getElementById("id_numbers").value;
    dict[11] = document.getElementById("ticket_numbers").value;
    dict[12] = document.getElementById("date_of_purchase").value;
    dict[13] = document.getElementById("Select1").value;
    dict[14] = document.getElementById("tirdz_viet_nos").value;
    dict[15] = document.getElementById("other_info").value; 
    dict[16] = day + "-" + month + "-" + year;
    return dict;
}


function send_values_to_db() {
    var fields = get_all_fields();
    PageMethods.Send_Field_Data(fields,fields["4"], success, fail);
    function success(result)
    {   
        if (result === true) {
            swal({
                title: "Dati veiksmīgi nosūtīti!",
                text: "Apstiprinājuma epasts nosūtīts uz: " + fields["4"],
                icon: "success",
                buttons: {
                    ok: {
                        text: "Ok",
                        value: "ok",
                    },
                }
            })
                .then((value) => {
                    switch (value) {

                        case "ok":
                            close();
                    }
                });
            console.log("Dati nosūtīti veiksmīgi! - " + result);
        }
        else
        {
            fail(result);
        }

    }
    function fail(result)
    {   
        swal("Something went wrong!", "Try again later", "error"); 
        console.log("Something went wrong! - " + result); 
    }
} 

//To show or hide info about what to write, when input field is focused

function focusFunction_ID() {
    document.getElementById("bilesu_id_info").style.display = "block";
}

function blurFunction_ID() {
    document.getElementById("bilesu_id_info").style.display = "none";
}


function focusFunction_VEID() {
    document.getElementById("bilesu_veid_info").style.display = "block";
}

function blurFunction_VEID() {
    document.getElementById("bilesu_veid_info").style.display = "none";
}    