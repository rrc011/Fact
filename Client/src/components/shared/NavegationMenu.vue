<template>
<el-menu default-active="0">
    <template v-for="item, i in items">
        <el-submenu :index="i.toString()" v-if="item.children !== undefined">
            <template slot="title">
                <i :class="item.icon"></i>
                <span>{{ item.text }}</span>
            </template>
            <el-menu-item-group title="Opciones">
                <el-menu-item v-for="child, c in item.children" :index="(i.toString() + c)" @click="redirect(child.path)">
                    <i :class="child.icon"></i> <span>{{ child.text }}</span>
                </el-menu-item>
            </el-menu-item-group>    
        </el-submenu>
        <el-menu-item index="2" v-if="item.children === undefined" @click="redirect(item.path)">
            <i :class="item.icon"></i>
            <span>{{ item.text }}</span>
        </el-menu-item>
    </template>
</el-menu>
</template>

<script>
export default {
  name: "NavegationMenu",
  data: () => ({
    items: [
      { icon: "fa fa-dashboard", text: "Dashboard", path: "/" },
      {
        icon: "fas fa-warehouse",
        text: "Almacenes",
        children: [
          { icon: "far fa-circle", text: "Listado de Almacenes", path: "/warehouse/page/1" }
        ]
      },
      {
        icon: "fas fa-th",
        text: "Productos",
        children: [
          { icon: "far fa-circle", text: "Listado de Productos", path: "/product/page/1" },
        ]
      },
      {
        icon: "fa fa-user",
        text: "Clientes",
        children: [
          { icon: "far fa-circle", text: "Listado de Clientes", path: "/client/page/1" }
        ]
      },
      {
        icon: "fas fa-dollar-sign",
        text: "Facturacion",
        children: [
          { icon: "far fa-circle", text: "Listado de Ventas", path: "/invoicing/page/1" }
        ]
      }
    ],

  }),
  methods: {
    redirect(path) {
      if(path === undefined) return;
      this.$router.push(path);
    }
  }
};
</script>