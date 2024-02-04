# Echo api

This API is used for testing purposes. It has only one `api/v1/echo` endpoint.

It will return the values sent in the post.

## How to deploy

Execute the following command:

```shell
docker build -t bernardbr/echo:1.0.0 -f Dockerfile .
```