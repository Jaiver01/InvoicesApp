let searchByClient = true;

function searchInvoices() {
  let invoiceNumber = $("#invoice-number").val();
  let invoiceClient = $("#invoice-client").val();

  let message = "";

  if (searchByClient && invoiceClient === "") {
    message = "Debes seleccionar un cliente para realizar la búsqueda";
  } else if (!searchByClient && invoiceNumber === "") {
    message = "Debes ingresar un número de factura para realizar la búsqueda";
  }

  if (message) {
    Swal.fire("", message, "warning");
    return;
  }

  getInvoices(invoiceClient, invoiceNumber);
}

async function getInvoices(invoiceClient, invoiceNumber) {
  const url = searchByClient
    ? `/Invoices/GetInvoicesByCustomer/${invoiceClient}`
    : `/Invoices/GetInvoicesByNumber/${invoiceNumber}`;

  try {
    const response = await fetch(url);
    const data = await response.json();

    const table = document.getElementById("invoices-table");
    const tableBody = table.getElementsByTagName("tbody")[0];
    tableBody.innerHTML = "";

    data.forEach((invoice) => {
      const row = tableBody.insertRow();

      const cellId = row.insertCell();
      const cellDate = row.insertCell();
      const cellTotal = row.insertCell();

      cellId.innerHTML = invoice.number;
      cellDate.innerHTML = new Date(invoice.date).toLocaleString();
      cellTotal.innerHTML = format(invoice.total);
    });
  } catch (error) {
    Swal.fire(
      "Error",
      "Ocurrió un problema al cargar los datos, intenta nuevamente",
      "error"
    );

    console.error("Error:", error);
  }
}

function format(number) {
  const formatter = new Intl.NumberFormat("es-CO");
  return formatter.format(parseFloat(number.toFixed(2)));
}

$(document).ready(function () {
  $('input[type=radio][name="radio-option"]').change(function () {
    searchByClient = this.value === "client";
    $("#invoice-client").prop("disabled", !searchByClient);
    $("#invoice-number").prop("disabled", searchByClient);
  });
});
