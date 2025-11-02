import { ReactMouseEvent } from "../../../../Shared/Types";

export interface IconButtonBase {
    size?: number;
    className?: string;
    isDisabled?: boolean;
    onClick?: (event: ReactMouseEvent) => void;
    onMouseDown?: (event: ReactMouseEvent) => void;
}

export interface IconButtonProps extends IconButtonBase {
    children: React.ReactElement | React.ReactElement[];
    hasNoHoverEffect?: boolean;
    hasGreyBackground?: boolean;
}

export interface IconButtonSolidProps extends IconButtonBase {
    name: string;
}
