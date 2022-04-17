export class JwtModel {
  constructor(public token: string, public expirationDate: Date) {}
}
