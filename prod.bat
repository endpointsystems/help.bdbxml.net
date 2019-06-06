pushd .\_site
call gsutil -h "Cache-Control:public, max-age=3600" -m cp -z "html,js,css" -r . gs://help.bdbxml.net
popd