# 知识点
0. 2D游戏，设置Camera的Projection为Orthographic，Default Behavior Mode为2D模式
1. 图片精灵：Sprite Mode设置为Multiple模式，Sprite Editor中点击Slice切割图片
2. 屏幕中物体的层级通过Sorting Layer和Order In Layer调节
3. SpringJoint2D组件：连接两个物体，做弹簧似的往返运动。
    - 给小鸟设置SpringJoint2D组件，给右边弹弓设置RigidBody2D组件，设置type为static，不受重力影响。
    - SpringJoint2D组件设置：设置Distance（距离）、Frequency(弹簧的强度)
4. 给小鸟添加`Box Colider`组件，可以点击；让小鸟的位置随鼠标移动
    - 先让屏幕坐标转化为世界坐标`Camera.main.ScreenToWordPoint(Input.mousePosition)`
5. 给右边的弹弓right添加一个rightPos的空子物体，用来设置位置；在小鸟和rightPos之间设置一个最大的距离。
6. 小鸟飞出
    - isKinematic 在按下时设置为true，抬起时设置为false （为了减缓飞出去的速度，按下后不计算动力，鼠标抬起才计算）
    - 0.1s后设置`SpringJoint2D`组件的enable为false（为了小鸟飞出去有速度，也就是在鼠标抬起时，给0.1s的时间让小鸟形成一个动力飞出去）
7. 猪的受伤
    - 创建一个空物体，创建地面（加上Box Colider，与小鸟有碰撞，12份）
    - 给小鸟加个在地面上滚动的阻力，也就是空气阻力，将`Angular drag`设置为2
    - 在场景中添加小猪、添加RigidBody 2D 和Circle Colider 2D的组件，并将Angular drag设置为2
    - 给猪加脚本，在`OnCollisonEnter2D`里检测与小鸟的碰撞，当碰撞后的相对速度大于最大值，则销毁小猪；大于最小速度，则让小猪受伤（让小猪的图片sprite改成104）
8. 弹弓划线操作
    - 给左右弹弓加个`Line Renderer`的组件，并将其颜色改为皮带的颜色，大小改为皮带的大小0.25
    - Bird的update中每次位置改变，从两个弹弓到小鸟之间绘制两条直线作为皮带
9. 死亡、加分特效的制作
    - 找到那几个动画的图片拖到场景中，制作成动画boom，让动画的Loop Time设置为false；给boom加脚本，添加一个消失的方法；在Animation中的最后一帧，添加消失的事件；制作成prefab
    - 给猪死亡后加上爆炸的效果
    - 猪死后，在猪的头顶加上分值，将分值制作成prefab
10. 游戏逻辑的判定、实现多只小鸟的飞出
    - 加一个GameManager，管理游戏（3只小鸟，2只猪）
    - GameManager的初始化方法：启用第一只小鸟的脚本和SpringJoint2D，其它小鸟都禁用
    - 在Bird脚本中，当小鸟飞出去后，移除并销毁小鸟；在GameManager的Nextbird方法中判定游戏是否结束、成功、或拉去下一只小鸟
11. 解决划线问题、小鸟间转换速度问题
    - 在GameManager中记录第一个小鸟的位置，每次初始化时，都将那个位置赋值给集合中的第一个小鸟，这样小鸟就不会受弹力的作用，猛的升上去
    - 在鼠标抬起时，将划线的组件禁止，在调用划线方法Line开始出启用划线组件
12. 添加小鸟飞出去的拖尾效果
    - 引入拖尾的package，给小鸟添加一个子空物体trail
    - trail中加入weapon脚本组件，修改material材质以及其它属性
    - bird中加入一个BirdTrail的脚本
    - 小鸟飞出去时，开启拖尾；小鸟碰撞地面或小猪后，结束拖尾
13. 整合场景，解决无法显示划线弹弓的问题
    - 选一个木块的图片拖到场景中woodCube，添加RigidBody2D和Box Colider2D以及Pig脚本，制作成prefab
    - 同样的远离，设置长木块、石头，石头的mass/maxSpeed/minSpeed设置的大些
    - 将猪、木块、石头组合成一个场景（放置在Player层）
    - 添加蓝天背景和小菜（看不见，就改变Sorting Layer）
    - 弹弓皮带无法显示，则改变天空背景的`Order in Layer`，改为-1
14. 添加失败、胜利的游戏UI界面
    - 将canvas的`UI Scale Mode`改为`Scale With Screen Size`
    - canvas/image，添加lose背景（按住alt键，填满屏幕）
    - lose下面再创建一个image，按住alt键上下填满，按住alt键左右放大，改变颜色和透明度
    - 在canvas中再添加一个retry按钮、一个home按钮、一个猪笑脸
    - 给lose UI的背景加个变暗的动画
    - 同样的原理复制一份游戏胜利的UI界面win
    - 在胜利的UI界面中添加一个脚本，在动画结束时，添加一个事件方法，调用Show()，显示星星
    - 在GameManager中，游戏失败，显示失败UI界面；游戏胜利，显示成功UI界面
15. 修改火花粒子系统
    - 把三颗星星加入到win的UI界面中，并调整它们的位置，开始时不显示
    - 导入root的package，创建一个空对象roots，把root下的物体拖拽到roots下（不需要root的效果），把Scaling Mode调整为Local（这样修改scale才会有效果），然后再修改transform中scale的x,y,z都为0.5，Start size也都减半