import { ReactMouseEventHandler } from "Shared/Types";

export interface LinkProps {
    to: string | undefined;
    className?: string;
    isDisabled?: boolean;
    rel?: string;
    onMouseEnter?: ReactMouseEventHandler;
    onMouseLeave?: ReactMouseEventHandler;
    children: React.ReactElement | React.ReactElement[];
}
