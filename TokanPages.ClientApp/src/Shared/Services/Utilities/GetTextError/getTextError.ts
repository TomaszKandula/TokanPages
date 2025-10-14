import { GetTextErrorProps } from "./Types";

export const GetTextError = (props: GetTextErrorProps): string => {
    return props.template.replace("{ERROR}", props.error);
};
