function InputMax(maxValues) {
    var amountSpentInput = document.getElementById("AmountSpent");
    amountSpentInput.value = GetMaxInputAmount(maxValues);
}

function InputHalf(maxValues) {
    var amountSpentInput = document.getElementById("AmountSpent");
    amountSpentInput.value = (GetMaxInputAmount(maxValues) / 2);
}

function GetMaxInputAmount(maxValues) {
    var sel = document.getElementById("SpendingCurrency");
    var cryptoCurrency = sel.options[sel.selectedIndex].value;
    return maxValues[cryptoCurrency];
}