//Debounce function to limit the rate at which a function can fire
window.debounceInput = (dotNetHelper, inputId, delay) => {
    let timer;
    const input = document.getElementById(inputId);
    input.addEventListener('input', () => {
        clearTimeout(timer);
        timer = setTimeout(() => {
            dotNetHelper.invokeMethodAsync('OnDebouncedInput', input.value);
        }, delay);
    });
};


/*

//Debounce function to limit the rate at which a function can fire
window.dotNetDebounce = {
    // Recebe o objeto .NET (por exemplo: @ref ao componente) e chama o método
    // "SearchReceiverAsync" depois de x milissegundos sem novas chamadas.
    create: function (dotNetHelper, dotNetMethodName, delay) {
        let timeoutId = null;
        return function (value) {
            if (timeoutId) {
                clearTimeout(timeoutId);
            }
            timeoutId = setTimeout(() => {
                dotNetHelper.invokeMethodAsync(dotNetMethodName, value);
                timeoutId = null;
            }, delay);
        };
    }
};

*/
