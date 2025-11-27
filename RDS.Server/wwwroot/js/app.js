function CompanyMaskInit() {
    var cleaveCNPJ, cleaveCEP;
    //CEP: 00.000-000
    //CNPJ: 00.000.000/0001-00
    //CPF: 000.000.000-00

    document.querySelector("#cnpj") && (cleaveDelimiters = new Cleave("#cnpj", { delimiters: [".", ".", "/", "-"], blocks: [2, 3, 3, 4, 2], uppercase: !0 }))
    document.querySelector("#cep") && (cleaveDelimiters = new Cleave("#cep", { delimiters: [".", "-"], blocks: [2, 3, 3], uppercase: !0 }))
    document.querySelector("#cpf") && (cleaveDelimiters = new Cleave("#cpf", { delimiters: [".", "-", "."], blocks: [3, 3, 3, 2], uppercase: !0 }))
}

window.cepMask = {
    apply: function (element) {
        element.addEventListener("input", function (e) {
            let value = e.target.value.replace(/\D/g, ""); // remove tudo que não é número

            if (value.length > 2) {
                value = value.slice(0, 2) + "." + value.slice(2);
            }
            if (value.length > 6) {
                value = value.slice(0, 6) + "-" + value.slice(6, 9);
            }

            e.target.value = value.slice(0, 10); // evita mais de 10 caracteres (incluindo . e -)
        });
    }
};

/*
window.currencyMask = {
    apply: function (element) {
        element.addEventListener("input", function (e) {
            let value = e.target.value.replace(/\D/g, "");
            value = (parseInt(value) / 100).toFixed(2) + "";
            value = value.replace(".", ",");
            value = value.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
            e.target.value = value;
        });
    }
};*/


window.currencyMask = {
    format: function (el) {
        // remove tudo que não é dígito
        let digits = el.value.replace(/\D/g, "");
        // transforma em centavos -> divide por 100
        let number = (parseInt(digits || "0", 10) / 100).toFixed(2);
        // usa vírgula decimal e pontos de milhar
        number = number.replace(".", ",");
        number = number.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
        el.value = number;
    }
};

window.phoneMask = {
    apply: function (elementId) {
        const input = document.getElementById(elementId);
        if (!input) return;

        // Formata o valor inicial
        this.formatInput(input);

        input.addEventListener('input', (e) => this.formatInput(e.target));
    },

    formatInput: function (input) {
        let value = input.value.replace(/\D/g, '');
        let formatted = '';

        if (value.length > 0) {
            formatted = '(' + value.substring(0, 2);
        }
        if (value.length > 2) {
            // Verifica se é celular (9 dígitos) ou fixo
            const part2Length = value.length > 10 ? 5 : 4;
            formatted += ') ' + value.substring(2, 2 + part2Length);
        }
        if (value.length > 6) {
            const part2Length = value.length > 10 ? 5 : 4;
            formatted += '-' + value.substring(2 + part2Length, 6 + part2Length);
        }

        input.value = formatted;

        // Dispara o evento 'change' para notificar o Blazor
        input.dispatchEvent(new Event('change'));
    }
};

window.setFocus = (fieldId) => {
    const el = document.getElementById(fieldId);
    if (el) el.focus();
}

