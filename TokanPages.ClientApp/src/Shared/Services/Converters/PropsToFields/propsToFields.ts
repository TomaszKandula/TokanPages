interface Properties {
    object: any;
}

export const PropsToFields = (props: Properties): any[] => {
    let resultArray: any[] = [];
    for (let Property in props.object) resultArray = resultArray.concat(props.object[Property]);
    return resultArray;
};
