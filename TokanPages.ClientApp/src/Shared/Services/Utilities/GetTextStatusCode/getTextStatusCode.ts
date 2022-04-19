import { UNEXPECTED_STATUS } from "../../../../Shared/constants";
import { IGetTextStatusCode } from "./interface";

export const GetTextStatusCode = (props: IGetTextStatusCode): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", props.statusCode.toString());
}
