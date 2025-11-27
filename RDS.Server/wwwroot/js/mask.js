// mask.js
window.applyCpfMask = (element) => {
    // The element passed from Blazor is the component's root div.
    // We need to find the actual <input> inside it.
    if (element) {
        const inputElement = element.querySelector('input');
        if (inputElement) {
            const maskOptions = {
                mask: '000.000.000-00'
            };
            const mask = IMask(inputElement, maskOptions);
        } else {
            console.error("Mask.js: Could not find the input element inside the provided component reference.");
        }
    } else {
        console.error("Mask.js: The element reference passed from Blazor was null.");
    }
};
