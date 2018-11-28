<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
<title>Biļešu Serviss</title>
	<meta charset="UTF-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
	<link rel="stylesheet" type="text/css" href="main.css"/>
	<script type="text/javascript" src="js.js"></script> 
    <script type="text/javascript" src="sweetalert.min.js"></script> 

<!--===============================================================================================-->
</head>
<body>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    
	<div class="container-contact100" id="formas_lauks">
		<div class="wrap-contact100">
			<form class="contact100-form validate-form">
				<span class="contact100-form-title">
					AKTS BIĻEŠU UN NAUDAS ATGRIEŠANAI
				</span> 
				<h2 id="apaksvirsraksts">(Naudas atgriešana ar pārskaitījumu)</h2>
				<h2>Informācija par pasākumu</h2>
				<div class="wrap-input100 validate-input" >
					<span class="label-input100">Pasākuma nosaukums</span>
					<input class="input100" type="text" name="name" placeholder="Ierakstiet pasākuma nosaukumu..." id="pasakuma_nosaukums" autocomplete="off">
					<span class="focus-input100"></span>
				</div>

				<div class="wrap-input100 validate-input">
					<span class="label-input100">Pasākuma datums</span>
					<input type="date" name="pasakuma_datums" style="height: 28px" class="input100" id="pasakuma_datums">
					<span class="focus-input100"></span>
				</div>
				<h2>Informācija par pircēju</h2>
				<div class="wrap-input100 validate-input">
						<span class="label-input100">Vārds/Uzvārds</span>
						<input class="input100" type="text" name="name" placeholder="Ierakstiet savu vārdu un uzvārdu..." id="vards_uzvards" autocomplete="off">
						<span class="focus-input100"></span>
				</div>

				<div class="wrap-input100 validate-input">
						<span class="label-input100">Personas kods</span>
						<input class="input100" type="text" name="name" placeholder="Jūsu personas kods..." id="personas_kods" autocomplete="off">
						<span class="focus-input100"></span>
				</div>
				<div class="wrap-input100 validate-input">
						<span class="label-input100">E-pasts</span>
						<input class="input100" type="text" name="name" placeholder="Jūsu e-pasts..." id="kontakti_epasts" autocomplete="off">
						<span class="focus-input100"></span>
				</div>

                		<div class="wrap-input100 validate-input">
						<span class="label-input100">Telefona numurs</span>
						<input class="input100" type="text" name="name" placeholder="Jūsu telefona numurs..." id="kontakti_tel" autocomplete="off">
						<span class="focus-input100"></span>
				</div>

					<h2>Konts</h2>

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Bankas konta numurs</span>
							<input class="input100" type="text" name="name" placeholder="Jūsu bankas konta numurs..." id="bank_number" autocomplete="off">
							<span class="focus-input100"></span>
					</div> 

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Bankas swift kods   </span>
							<input class="input100" type="text" name="name" placeholder="Jūsu bankas swift kods..." id="swift_code" autocomplete="off">
							<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Bankas nosaukums</span>
							<input class="input100" type="text" name="name" placeholder="Jūsu bankas nosaukums..." id="bank_name" autocomplete="off">
							<span class="focus-input100"></span>
					</div> 

					<h2>Informācija par biļetēm</h2>

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Biļetes cena</span>
							<input class="input100" type="text" name="name" placeholder="Biļetes cena..." id="ticket_price" autocomplete="off">
							<span class="focus-input100"></span>
					</div> 

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Biļetes ID numurs</span>
							<input autocomplete="off" class="input100" type="text" name="name" placeholder="Biļetes ID numurs..." id="id_numbers" onfocus="focusFunction_ID()" onblur="blurFunction_ID()"> 
                            <span class="info" id="bilesu_id_info" style="display:none">ID numurs – astoņu vai deviņu melnas krāsas ciparu numurs biļetes augšējās malas kreisajā pusē</span>
							<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Biļetes veidlapas numurs</span>
							<input autocomplete="off" class="input100" type="text" name="name" placeholder="Biļetes veidlapas numurs..." id="ticket_numbers" onfocus="focusFunction_VEID()" onblur="blurFunction_VEID()">
						    <span class="info" id="bilesu_veid_info" style="display:none">Biļetes veidlapas numurs biļetei, kas pirkta kasē –  septiņu melnas (iespējams arī zelta vai sarkanas) krāsas ciparu numurs biļetes apakšējās malas kreisajā pusē</span>	
                        <span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input">
							<span class="label-input100">Pirkuma datums</span>
							<input type="date" name="pirkuma_datums" style="height: 28px" class="input100" id="date_of_purchase">
							<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 input100-select">
						<span class="label-input100">Iegādes vieta</span>
						<div>
							<select id="Select1" name="iegādes_vieta"  onchange="show_if_selected()">
								<option value="I-Veikals">I-Veikals</option>
								<option value="Tirdzniecības punkts">Tirdzniecības punkts</option>
							</select>
						</div>
						<span class="focus-input100"></span>
					</div> 

					<div class="wrap-input100 validate-input" id="tirdzniecibas_vieta_lauks" style="display:none">
						<span class="label-input100">Tirdzniecības punkta nosaukums</span>
						<input class="input100" type="text" name="name" id="tirdz_viet_nos" placeholder="Tirdzniecības vietas nosaukums..." autocomplete="off">
						<span class="focus-input100"></span>
					</div>
					

				<div class="wrap-input100 validate-input">
					<span class="label-input100">Cita informācija no pircēja</span>
					<textarea class="input100" name="message" placeholder="Rēķina Nr:..." id="other_info"></textarea>
					<span class="focus-input100"></span>
				</div>

				<div class="container-contact100-form-btn">
					<div class="wrap-contact100-form-btn">
						<div class="contact100-form-bgbtn"></div>
						<p><input id="submit_button" type="button" value="Iesniegt" class="button" onclick="show_modal()";/></p>
					</div>
				</div>
			</form>
			    	<!-- The Modal -->
					<div id="myModal" class="modal">
							<!-- Modal content -->
							<div class="modal-content">
							  <div class="modal-header" id="modal_teksts">
								<h2>Pircēja norādītā informācija tiks izmantota tikai naudas atgriešanai ar pārskaitījumu
									Aizpildot šo aktu, pircējs piekrīt, ka viņa personas dati, kontaktinformācija tiks izmantota naudas pārskaitījuma veikšanai.
								</h2>
								<h2>Vai piekrītat?</h2>
							  </div>
							  <div class="modal-body">
								<div> 
									<input type="button" value="Jā" class="button2" onclick="nosutam_datus()"/>
									<input type="button" value="Nē" class="button2" onclick="hide_modal()"/>
								</div>
							  </div>
							</div>
						  </div>
		</div>
	</div>


        </form>
</body>
</html>
