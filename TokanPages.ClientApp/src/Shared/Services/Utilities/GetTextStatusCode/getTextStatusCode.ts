import { UNEXPECTED_STATUS } from "../../../../Shared/constants";
import { GetTextStatusCodeInput } from "./interface";

export const GetTextStatusCode = (props: GetTextStatusCodeInput): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", props.statusCode.toString());
}
