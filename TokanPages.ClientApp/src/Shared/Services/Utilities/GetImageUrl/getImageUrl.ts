interface GetImageUrlProps {
    base: string;
    name: string;
}

export const GetImageUrl = (props: GetImageUrlProps): string | undefined => {
    if (props.name === "") return undefined;
    return `${props.base}/${props.name}`;
};
