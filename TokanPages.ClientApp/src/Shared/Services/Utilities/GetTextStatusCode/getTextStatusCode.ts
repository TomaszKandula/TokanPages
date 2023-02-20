import { UNEXPECTED_STATUS } from "../../../../Shared/constants";

interface Properties
{
    statusCode: number;
}

export const GetTextStatusCode = (props: Properties): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", props.statusCode.toString());
}
