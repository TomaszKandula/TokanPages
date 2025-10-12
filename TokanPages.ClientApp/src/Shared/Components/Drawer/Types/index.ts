export interface DrawerProps {
    isOpen: boolean;
    isExternalClose?: boolean;
    onTrigger: () => void;
    children: React.ReactElement | React.ReactElement[];
}
