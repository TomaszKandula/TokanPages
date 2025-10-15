import { ReactCSSProps, ReactMouseDivEventHandler } from "../../../../Shared/Types";

export interface StandardBackdropProps {
    className?: string;
    style?: ReactCSSProps | undefined;
    onClick?: ReactMouseDivEventHandler | undefined;
}
