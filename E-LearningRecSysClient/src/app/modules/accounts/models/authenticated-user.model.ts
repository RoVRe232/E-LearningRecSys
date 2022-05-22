import { JwtModel } from './jwt.model';

export class AuthenticatedUserModel {
  constructor(
    public userID: string,
    public accountID: string,
    public firstName: string,
    public lastName: string,
    public email: string,
    public role: string,
    public authToken?: JwtModel,
    public refreshToken?: JwtModel,
  ) {}
}
