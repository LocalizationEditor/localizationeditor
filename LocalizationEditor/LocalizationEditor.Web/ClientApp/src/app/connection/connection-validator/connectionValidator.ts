export class ConnectionValidator {
  maxLength: number;
  minLength: number;

  constructor(maxLength: number, minLength: number) {
    this.maxLength = maxLength;
    this.minLength = minLength;
  }

  isValid(text: string): boolean {
    return !(text.length <= this.minLength || text.length > this.maxLength);
  }
}
