import { JwtModel } from './jwt.model';

export class AuthenticatedUserModel {
  constructor(
    public userId: string,
    public accountId: string,
    public firstName: string,
    public lastName: string,
    public email: string,
    public role: string,
    public authToken?: JwtModel,
    public refreshToken?: JwtModel,
  ) {}
}
