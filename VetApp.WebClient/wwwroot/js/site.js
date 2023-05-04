// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var totalEntryValue;
function getAllElementsAsArray(selector) {
  return Array.prototype.slice.call(document.querySelectorAll(selector));
}

function getValuesFromElements(selector) {
  return getAllElementsAsArray(selector).map((el) => parseFloat(el.innerHTML))
}

const calculateTotalOfInventoryEntry = (arrayOfPrices) => {
  if (!arrayOfPrices.length) return 0;
  return arrayOfPrices.filter(el => !!el).reduce(
      (prev, curr) => parseFloat(prev) + parseFloat(curr)
    );
};


const InventoryDetailsTotal = function _InventoryDetailsTotal() {
  const total =  _InventoryDetailsTotal.state.total.toFixed(2);
  return `
  <h5>Целосна сума: <span id="overallTotal">${total}</span><span>&nbsp; ден</span></h5>
  `;
};
const repaintTotal = () => {
  document.getElementById('total').innerHTML = InventoryDetailsTotal();

}
InventoryDetailsTotal.state = {
  total: 0,
  recalculate: () => {
    let arrayOfPrices = getValuesFromElements('#totalPrice');
    InventoryDetailsTotal.state.total = calculateTotalOfInventoryEntry(arrayOfPrices);
    repaintTotal();
  },
};


function getTotal() {
  if (!totalEntryValue) {
    return 0;
  }
  console.log(totalEntryValue);
  return totalEntryValue;
}

const featureNotImplementedYet = () => {
  toastr.options = {
    closeButton: true,
    debug: false,
    newestOnTop: false,
    progressBar: false,
    positionClass: 'toast-top-full-width',
    preventDuplicates: false,
    onclick: null,
    showDuration: '300',
    hideDuration: '1000',
    timeOut: '5000',
    extendedTimeOut: '1000',
    showEasing: 'swing',
    hideEasing: 'linear',
    showMethod: 'fadeIn',
    hideMethod: 'fadeOut',
  };
  toastr.info('Оваа функционалност се уште не е имплементирана.');
};
