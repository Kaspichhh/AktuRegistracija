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
        alert("Kaut kas nogāja greizi?"); 
        swal("Kaut kas nogāja greizi?", "", "warning");
    }
}

function nosutam_datus()
{
    hide_modal();
    validate_IBAN_number();
}

function get_all_fields() {
    var dict = {};
    dict[0] = document.getElementById("pasakuma_nosaukums").value;
    dict[1] = document.getElementById("pasakuma_datums").value;
    dict[2] = document.getElementById("vards_uzvards").value;
    dict[3] = document.getElementById("personas_kods").value;
    dict[4] = document.getElementById("kontakti").value;
    dict[5] = document.getElementById("bank_number").value;
    dict[6] = document.getElementById("swift_code").value;
    dict[7] = document.getElementById("bank_name").value;
    dict[8] = document.getElementById("ticket_price").value;
    dict[9] = document.getElementById("id_numbers").value;
    dict[10] = document.getElementById("ticket_numbers").value;
    dict[11] = document.getElementById("date_of_purchase").value;
    dict[12] = document.getElementById("Select1").value;
    dict[13] = document.getElementById("tirdz_viet_nos").value;
    dict[14] = document.getElementById("other_info").value;
    return dict;
}


function send_values_to_db() {
    var fields = get_all_fields();
    PageMethods.Send_Field_Data(fields, success, fail);
    function success(result)
    {   
        swal({
            title: "Dati veiksmīgi nosūtīti!",
            text: "Nospiežot 'OK' logs tiks aizvērts!",
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
        //alert("Dati nosūtīti veiksmīgi! - " + "Response from server: " + result);
        console.log("Dati nosūtīti veiksmīgi! - " + result);
    }
    function fail(result)
    {   
        swal("Something went wrong!", result, "error");
        //alert("Something went wrong! - " + "Response from server: " + result);
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