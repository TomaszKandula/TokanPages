export interface IconBaseProps {
    size?: number;
    onClick?: () => void;
}

export interface IconProps extends IconBaseProps {
    name: string;
    size: number;
    className?: string;
}
