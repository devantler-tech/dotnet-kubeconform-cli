#!/bin/bash
set -e

get() {
  local url=$1
  local binary=$2
  local target_dir=$3
  local target_name=$4
  local isTar=$5

  # check if tar
  if [ "$isTar" = true ]; then
    curl -LJ "$url" | tar xvz -C "$target_dir" "$binary"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
  elif [ "$isTar" = false ]; then
    curl -LJ "$url" -o "$target_dir/$target_name"
  fi
  chmod +x "$target_dir/$target_name"
}

get "https://getbin.io/yannh/kubeconform?os=darwin&arch=amd64" "kubeconform" "src/Devantler.KubeconformCLI/assets/binaries" "kubeconform-darwin-amd64" true
get "https://getbin.io/yannh/kubeconform?os=darwin&arch=arm64" "kubeconform" "src/Devantler.KubeconformCLI/assets/binaries" "kubeconform-darwin-arm64" true
get "https://getbin.io/yannh/kubeconform?os=linux&arch=amd64" "kubeconform" "src/Devantler.KubeconformCLI/assets/binaries" "kubeconform-linux-amd64" true
get "https://getbin.io/yannh/kubeconform?os=linux&arch=arm64" "kubeconform" "src/Devantler.KubeconformCLI/assets/binaries" "kubeconform-linux-arm64" true
