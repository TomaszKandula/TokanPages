import { ReactCSSProps, ReactMouseDivEventHandler } from "../../../../Shared/types";

export interface StandardBackdropProps {
    className?: string;
    style?: ReactCSSProps | undefined;
    onClick?: ReactMouseDivEventHandler | undefined;
}
