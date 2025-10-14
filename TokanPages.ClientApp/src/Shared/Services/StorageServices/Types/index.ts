interface BaseProps {
    key: string;
}

export interface DelDataFromStorageProps extends BaseProps {
}

export interface GetDataFromStorageProps extends BaseProps {
}

export interface SetDataInStorageProps extends BaseProps {
    selection: {} | any[];
}
