function validateNumericInput(event) {
    var charCode = event.which ? event.which : event.keyCode;

    
    if ((charCode < 48 || charCode > 57) && charCode !== 46) {
        event.preventDefault(); 
    }

   
    if (charCode === 46 && event.target.value.includes('.')) {
        event.preventDefault(); 
    }
}

function limitToTwoDecimalPlaces(input) {
    
    const regex = /^\d+(\.\d{0,2})?$/;
    if (!regex.test(input.value)) {
        input.value = input.value.slice(0, -1);
    }
}