import { ReactMouseEvent } from "../../../../Shared/Types";

export interface IconButtonProps {
    size?: number;
    children: React.ReactElement | React.ReactElement[];
    hasNoHoverEffect?: boolean;
    className?: string;
    isDisabled?: boolean;
    hasGreyBackground?: boolean;
    onClick?: (event: ReactMouseEvent) => void;
    onMouseDown?: (event: ReactMouseEvent) => void;
}
