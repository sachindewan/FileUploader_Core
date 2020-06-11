export class ValidationService {
  static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
    const config = {
      required: ` Required`,
      invalidPassword:
        'Invalid password. Password must be at least 4 characters long, and contain a number.',
      minlength: `Minimum length ${validatorValue.requiredLength}`,
      maxlength: `Maximum length ${validatorValue.requiredLength}`,
      mismatch: `password must match`,
    };

    return config[validatorName];
  }
}
