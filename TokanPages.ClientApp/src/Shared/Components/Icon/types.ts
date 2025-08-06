export interface IconBaseProps {
    size?: number;
    className?: string;
    onClick?: () => void;
}

export interface IconProps extends IconBaseProps {
    name: string;
    size: number;
    className?: string;
}
