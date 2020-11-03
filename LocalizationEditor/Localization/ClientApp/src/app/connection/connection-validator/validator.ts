export class Validator {
  isValid(text: string): boolean {
    return !(text.length <= 1 || text.length > 256);
  }
}
