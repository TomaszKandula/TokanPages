import { Method } from "axios";

export interface IApiRequest
{
    method: Method;
    url: string;
    data?: any;
}
