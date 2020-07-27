import { Injectable, LOCALE_ID } from '@angular/core';
import { getLocaleNumberFormat } from '@angular/common';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';

// TODO: separar validação
@Injectable({
  providedIn: 'root'
})
export class ValidatorService {
  constructor() {
    // TODO: region specific
    this.currencyMask = createNumberMask({
      prefix: 'R$ ',
      includeThousandSeparator: true,
      allowDecimal: true,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      decimalLimit: 8,
      requireDecimal: false,
      integerLimit: 12
    });

    this.unitPriceMask = createNumberMask({
      prefix: 'R$ ',
      includeThousandSeparator: true,
      allowDecimal: true,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      decimalLimit: 8,
      requireDecimal: false,
      integerLimit: 10
    });

    this.currencyMaskDoubleDecimal = createNumberMask({
      prefix: 'R$ ',
      includeThousandSeparator: true,
      allowDecimal: true,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      decimalLimit: 2,
      requireDecimal: false,
      integerLimit: 14
    });

    this.decimalMask = createNumberMask({
      prefix: '',
      includeThousandSeparator: true,
      allowDecimal: true,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      decimalLimit: 8,
      requireDecimal: false,
      integerLimit: 12
    });

    this.decimalFeeMask = createNumberMask({
      prefix: '',
      decimalLimit: 6,
      allowDecimal: true,
      requireDecimal: false,
      decimalSymbol: ',',
      integerLimit: 3
    });

    this.decimalFutureFeeMask = createNumberMask({
      prefix: '',
      decimalLimit: 8,
      allowDecimal: true,
      requireDecimal: false,
      decimalSymbol: ',',
      integerLimit: 4
    });

    this.decimalIndexFeeMask = createNumberMask({
      prefix: '',
      decimalLimit: 8,
      allowDecimal: true,
      requireDecimal: false,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      integerLimit: 10
    });

    this.decimalSixteenMask = createNumberMask({
      prefix: '',
      decimalLimit: 2,
      allowDecimal: true,
      requireDecimal: false,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      integerLimit: 16
    });

    this.amountMask = createNumberMask({
      prefix: '',
      decimalLimit: 0,
      allowDecimal: false,
      requireDecimal: false,
      thousandsSeparatorSymbol: '.',
      decimalSymbol: ',',
      integerLimit: 10
    });

    this.percentMask = createNumberMask({
      prefix: '',
      suffix: ' %',
      includeThousandSeparator: false,
      allowDecimal: true,
      thousandsSeparatorSymbol: '',
      decimalSymbol: ',',
      decimalLimit: 8,
      requireDecimal: false,
      integerLimit: 12
    });
  }

  private blacklisted = [
    '00000000000000',
    '11111111111111',
    '22222222222222',
    '33333333333333',
    '44444444444444',
    '55555555555555',
    '66666666666666',
    '77777777777777',
    '88888888888888',
    '99999999999999',
    '00000000000',
    '11111111111',
    '22222222222',
    '33333333333',
    '44444444444',
    '55555555555',
    '66666666666',
    '77777777777',
    '88888888888',
    '99999999999'
  ];

  public readonly documentLegalPersonMask = [
    /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/];

  public readonly currencyMask: any;

  public readonly unitPriceMask: any;

  public readonly currencyMaskDoubleDecimal: any;

  public readonly decimalMask: any;

  public readonly decimalFeeMask: any;

  public readonly decimalFutureFeeMask: any;

  public readonly decimalIndexFeeMask: any;

  public readonly decimalSixteenMask: any;

  public readonly amountMask: any;

  public readonly percentMask: any;

  public readonly numericMask = [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];

  public readonly dateMask = [/\d/, /\d/, '/', /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/];

  public readonly documentPhysicalPersonMask =
  [/[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '-', /[0-9]/, /[0-9]/];

  // TODO: region specific
  public getCurrency(str: string | number): number {
    return Number((str || '').toString().replace('R$ ', '').replace(/[.]/g, '').replace(/,/g, '.'));
  }

  public getNumeric(str: string | number): number {
    return Number((str || '').toString().replace('R$ ', '').replace(/[^,\d]+/g, '').replace(/,/g, '.'));
  }

  public sanitize(str: string) {
    return str.replace(/[^a-zA-Z0-9]/g, '');
  }

  private verifierDigit(numbers) {
    let index = 2;
    const reverse = numbers.split('').reduce(function (buffer, number) {
        return [parseInt(number, 10)].concat(buffer);
    }, []);

    const sum = reverse.reduce(function (buffer, number) {
        buffer += number * index;
        index = (index === 9 ? 2 : index + 1);
        return buffer;
    }, 0);

    const mod = sum % 11;
    return (mod < 2 ? 0 : 11 - mod);
  }

  private strip(number) {
      return (number || '').toString().replace(/\D/g, '');
  }

  public formatDocument(documento: string): string {
    const onlyDigits = (documento || '').replace(/\D/g, '');
    const formatted = onlyDigits.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');

    if (onlyDigits === formatted) {
        return documento;
    }
    return formatted;
}

  public validateDocument(number: string): boolean {
      const stripped = this.strip(number);

      // CNPJ must be defined
      if (!stripped) { return false; }

      // CNPJ must have 14 chars
      if (stripped.length !== 14) { return false; }

      // CNPJ can't be blacklisted
      if (this.blacklisted.includes(stripped)) { return false; }

      let numbers = stripped.substr(0, 12);
      numbers += this.verifierDigit(numbers);
      numbers += this.verifierDigit(numbers);

      return numbers.substr(-2) === stripped.substr(-2);
  }

  public validatePhysicalDocument(cpf: string): boolean {
    cpf = this.strip(cpf);

    if (cpf == null) {
      return false;
    }
    if (cpf.length !== 11) {
        return false;
    }

    if (this.blacklisted.includes(cpf)) { return false; }

    let numero = 0;
    let caracter = '';
    const numeros = '0123456789';
    let j = 10;
    let somatorio = 0;
    let resto = 0;
    let digito1 = 0;
    let digito2 = 0;
    let cpfAux = '';

    cpfAux = cpf.substring(0, 9);

    for (let i = 0; i < 9; i++) {
        caracter = cpfAux.charAt(i);
        if (numeros.search(caracter) === -1) {
            return false;
        }
        numero = Number(caracter);
        somatorio = somatorio + (numero * j);
        j--;
    }
    resto = somatorio % 11;
    digito1 = 11 - resto;
    if (digito1 > 9) {
        digito1 = 0;
    }
    j = 11;
    somatorio = 0;
    cpfAux = cpfAux + digito1;
    for (let i = 0; i < 10; i++) {
        caracter = cpfAux.charAt(i);
        numero = Number(caracter);
        somatorio = somatorio + (numero * j);
        j--;
    }
    resto = somatorio % 11;
    digito2 = 11 - resto;
    if (digito2 > 9) {
        digito2 = 0;
    }
    cpfAux = cpfAux + digito2;
    if (cpf !== cpfAux) {
        return false;
    } else {
        return true;
    }
  }
}
