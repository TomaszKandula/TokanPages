export interface Fields {
    id: string;
    type: string;
    value: string;
    link?: string;
    icon?: string;
    enabled?: boolean;
    sideMenu?: MenuProps;
    navbarMenu?: MenuProps;
}

export interface MenuProps {
    enabled: boolean;
    sortOrder: number;
}
